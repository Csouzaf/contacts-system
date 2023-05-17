import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Users } from '../Users';
@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  private contactsURL = "https://localhost:7087/api/contacts";

    httpOptions =
    {
      Headers: new HttpHeaders({ 'contenty-type' : 'application/json' } )
    }

  constructor(private http: HttpClient) { }

    getUsers(): Observable<Users[]>
    {
      return this.http.get<Users[]>(this.contactsURL);
    }
}
