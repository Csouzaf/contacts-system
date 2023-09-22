
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from 'models/Login';

import { Observable, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthLoginService {

  private authLoginURL = "https://localhost:7087/api/auth/login";



  constructor(private http : HttpClient) { }

  HttpOptions = {
    headers: new HttpHeaders( {
      'Contenty-Type': 'application/json',
      'Accept':'application/json'

    }), withCredentials: true


  }


  sendPostLogin(login: Login): Observable<Login> {

   return this.http.post<Login>(this.authLoginURL, login, this.HttpOptions)
      .pipe(

        catchError((error) => {

          if(error.status === 400 || error.status === 404 || error.status === 402 || error.status === 403 || error.status === 500) {
            alert("Usuário não encontrado");
          }

          return of();
        })
      );

  };

}
