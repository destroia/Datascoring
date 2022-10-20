import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../Services/user.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {

  constructor(private fb: FormBuilder, private servicesUser: UserService,
    private router: Router) { }
  formulario!: FormGroup;
  user: any = null;
  checkPasswords: boolean = true;
  users: any[] = [];
  userIsUpdate: boolean = false;
  userIsChangedPassword: boolean = false;
  strState: string = "Crear Nuevo Usuario";
  ngOnInit(): void {
    this.InitForm();
    this.LoadInfo();
  }
  strCreate: string = "";
  InitForm() {

    this.formulario = this.fb.group(
      {
        id: [this.user !== null ? this.user.id : 0],
        name: [this.user !== null ? this.user.name : "", Validators.compose(
          [Validators.required,
          Validators.maxLength(200),
          Validators.minLength(2)])],
        lastName: [{value:this.user !== null ? this.user.lastName : "", disabled: false}, Validators.compose(
          [Validators.required,
          Validators.maxLength(200),
          Validators.minLength(2)])],
        email: [this.user !== null ? this.user.email : "", Validators.compose(
          [Validators.required,
          Validators.maxLength(50),
          Validators.minLength(2)])],
        password: [this.user !== null ? "*********":"", Validators.compose(
          [Validators.required,
          Validators.maxLength(50),
            Validators.minLength(8)])],
        confirmPassword: [{value:this.user !== null ? "*********" : ""}, Validators.compose(
          [Validators.required,
          Validators.maxLength(50),
          Validators.minLength(8)])],
      });
    if (this.user === null) {
      this.strState = "Crear Nuevo Usuario";
    }
    //if (this.userIsUpdate) {
    //  this.formulario.controls["password"].disable();
    //  this.formulario.controls["confirmPassword"].disable();
    //} else {
    // // this.formulario.controls["password"].enabled;
    //  //this.formulario.get("confirmPassword")?.enable()   //.enable();
    //}
 
    //if (this.userIsChangedPassword) {
      
    //  this.formulario.controls["name"].disable();
    //  this.formulario.controls["lastName"].disable();
    //  this.formulario.controls["email"].disable();
    //  this.formulario.controls["password"].setValue("");
    //  this.formulario.controls["confirmPassword"].setValue("");
    //} else {
    //  this.formulario.controls["name"].enable() ;
    //  this.formulario.controls["lastName"].enable();
    //  this.formulario.controls["email"].enable();
    //}


  }
  CheckPasswords() {
    let pass = this.formulario.controls["password"].value;
    let confirmPass = this.formulario.controls["confirmPassword"].value;
    this.checkPasswords = pass === confirmPass ? true : false;
  }
  LoadInfo() {
    this.users = [];
    this.servicesUser.Get().subscribe(x => this.ConfirmGet(x),err => console.log(err))
  }
  ConfirmGet(x: any[]): void {
    
      this.users = x;
    }
  Save() {
    if (this.userIsChangedPassword)
    {
      let changed: any = {
        userId: Number(this.formulario.controls["id"].value),
        newPassword: this.formulario.controls["password"].value
      }
      this.servicesUser.ChangePassword(changed).subscribe(x => this.ConfirmCreate(x), err => this.ErrorCreate(err))
    }
    else {
      let user: any =
      {
        id: Number(this.formulario.controls["id"].value),
        name: this.formulario.controls["name"].value,
        lastName: this.formulario.controls["lastName"].value,
        email: this.formulario.controls["email"].value,
        password: this.formulario.controls["password"].value
      }

      if (user.id === 0) {
        this.servicesUser.Create(user).subscribe(x => this.ConfirmCreate(x), err => this.ErrorCreate(err))
      } else {
        this.servicesUser.Update(user).subscribe(x => this.ConfirmUpdate(x), err => this.ErrorCreate(err))
      }
    }
  }
    ConfirmUpdate(x: boolean): void {
      this.strCreate = "";
      this.userIsUpdate = false;
      this.user = null;
      this.InitForm();
      this.LoadInfo();

    }
  ErrorCreate(err: any): void {
    console.log(err)
    if (err.error.value) {
      this.strCreate = err.error.mjs;
    }
   
    }
    ConfirmCreate(x: boolean): void {
      this.strCreate = "";
      this.user = null;

      this.userIsChangedPassword = false;
      this.userIsUpdate = false;
      this.InitForm();
      this.LoadInfo();
  }
  Update(li: any) {
    this.user = li;
    this.userIsChangedPassword = false;
    this.userIsUpdate = true;
    this.strCreate = "";
    this.strState = "Actualizar Usuario"
    this.strUserData = "Actualizar  Usuario : " + li.name + " " + li.lastName;
    this.InitForm();
  }
  strUserData: string = "";
  Delete(li: any) {
 
    this.strCreate = "";
    this.servicesUser.Delete(li.id).subscribe(x => this.ConfirmUpdate(x), err => this.ErrorCreate(err))
  }
  ChangedPassword(li: any) {
    this.user = li;
    this.strUserData = "Actualizar  la contraseña del Usuario : " + li.name + " " + li.lastName;
    this.strState = "Actualizar Contraseña"
    this.strCreate = "";
    this.userIsUpdate = false;
    this.userIsChangedPassword = true;
    this.InitForm();
  }
  Back() { this.router.navigate([""]) }
}
