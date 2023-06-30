import { Injectable } from '@angular/core';
import { Route, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthEnsureService {

  constructor(private router : Router) { }

  // canActivate(): boolean{

  // }
}
