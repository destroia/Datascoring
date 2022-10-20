import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../Services/user.service';

@Component({
  selector: 'app-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.css']
})
export class PagesComponent implements OnInit {

  constructor(private fb: FormBuilder, private servicesUser: UserService,
    private router: Router ) { }

  formulario!: FormGroup;
  ngOnInit(): void {
    this.InitForm();
  }
  strLogin: string = "";
  InitForm() {

    this.formulario = this.fb.group(
      {        
        email: ["", Validators.compose(
          [Validators.required,
          Validators.maxLength(50),
          Validators.minLength(2)])],
        password: ["", Validators.compose(
          [Validators.required,
          Validators.maxLength(50),
          Validators.minLength(8)])],
      });
  }

  Login() {
    let login: any = {
      email: this.formulario.controls["email"].value,
      password: this.formulario.controls["password"].value,

    }
    this.servicesUser.Login(login).subscribe(x => this.ConfirmLogin(x), err => console.log(err));
  }
    ConfirmLogin(x: any): void {
      if (x.value) {
        this.strLogin = x.obj.id + "  " + x.obj.email + "  " + x.obj.name + "  " + x.obj.lastName
      } else {
        this.strLogin = "El usuario no existe"
      }
  }

  CreateNewUser() {
    this.router.navigate(["Users"])
  }
}
