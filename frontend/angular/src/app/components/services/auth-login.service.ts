import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'models/Login';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthLoginService {

  login: Login[] =[];

  private autLoginURL = "https://localhost:7087/api/auth/login";

  HttpOtions ={
    headers: new HttpHeaders({

      'Contenty-type' : 'application/json'
    }),
    withCredencials: true
  }

  constructor(private http : HttpClient) { }

  sendPostLogin(login: Login) : Observable<Login>
  {
    return this.http.post<Login>(this.autLoginURL,login, this.HttpOtions)
  }

}
