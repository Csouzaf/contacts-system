import { Signup } from './../../../../models/Signup';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AuthRegisterService {

  signup: Signup[] = [];

  private authSignupURL = "https://localhost:7087/api/auth/signinup";

  HttpOptions =
  {
    headers: new HttpHeaders({
      'Contenty-type': 'application/json',

    })
  };


  constructor(private http: HttpClient) { }

  postRegister(signup: Signup): Observable<Signup>
  {
    return this.http.post<Signup>(this.authSignupURL, signup, this.HttpOptions)
  }


}
