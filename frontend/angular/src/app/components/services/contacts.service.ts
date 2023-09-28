import { Signup } from './../../../../models/Signup';
import { UserSharedService } from './user-shared.service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';
import { Users } from '../../../../models/Users';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  private contactsURL = "https://localhost:7087/api/contacts";
  private createContacts = "https://localhost:7087/api/contacts/create";

    httpOptions =
    {
      headers: new HttpHeaders({ 'Contenty-type':'application/json' } )
    }
    // private contactsSubject = new Subject<Users[]>();
    // contacts$ = this.contactsSubject.asObservable();

  constructor(private http: HttpClient) { }

    getUsers(): Observable<Users[]>
    {
      return this.http.get<Users[]>(this.contactsURL)
    }

    getUsersById(id: number): Observable<Users>
    {
      const apiUrl = `${this.contactsURL}/${id}`;
      return this.http.get<Users>(apiUrl);
    }

    postUsers(users: Users): Observable<Users>
    {
      return this.http.post<Users>(this.createContacts, users, this.httpOptions)

    }

    updateUsers(id: number, users: Users): Observable<any>
    {
      const update = `${this.contactsURL}/${id}`
      return this.http.put<Users>(update, users, this.httpOptions)
    }

    deleteUsers(id: number): Observable<any>
    {
      const removeUsersApi = `${this.contactsURL}/${id}`;
      return this.http.delete<number>(removeUsersApi, this.httpOptions)
    }


}
