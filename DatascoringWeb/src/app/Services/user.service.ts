import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }


  Get(): Observable<any[]> {
    return this.http.get<any[]>(environment.urlService + "Users/Get");
  }
  Create(user: any): Observable<boolean> {
    return this.http.post<boolean>(environment.urlService + "Users/Post",user);
  }
  Login(login: any): Observable<any> {
    return this.http.post<any>(environment.urlService + "Users/Login", login);
  }
  Update(user: any): Observable<boolean> {
    return this.http.put<boolean>(environment.urlService + "Users/Put", user);
  }
  ChangePassword(changed: any): Observable<boolean> {
    return this.http.put<boolean>(environment.urlService + "Users/ChangePassword", changed);
  }
  Delete(id: number): Observable<boolean> {
    return this.http.delete<boolean>(environment.urlService + "Users/Delete/" + id);
  }
}
