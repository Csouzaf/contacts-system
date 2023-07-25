import { Signup } from './../../../../models/Signup';
import { Observable, catchError, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AuthRegisterService {

  signup: Signup[] = [];

  private authSignupURL = "https://localhost:7087/api/auth/signup";

  HttpOptions =
  {
    headers: new HttpHeaders({
      'Contenty-type': 'application/json',

    }),
  };


  constructor(private http: HttpClient) { }

  postRegister(signup: Signup): Observable<Signup>
  {

    return this.http.post<Signup>(this.authSignupURL, signup, this.HttpOptions).pipe(

      catchError((error) => {

        if(error.status === 500){
          alert("Email já criado, tente outro")
        }
        return of();
      })
    );

  }


}
