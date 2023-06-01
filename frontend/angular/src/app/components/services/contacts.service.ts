import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Users } from '../../../../models/Users';
@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  private contactsURL = "https://localhost:7087/api/contacts";

    httpOptions =
    {
      headers: new HttpHeaders({ 'contenty-type' : 'application/json' } )
    }

  constructor(private http: HttpClient) { }

    getUsers(): Observable<Users[]>
    {
      return this.http.get<Users[]>(this.contactsURL);
    }

    getUsersById(id: number): Observable<Users>
    {
      const apiUrl = `${this.contactsURL}/${id}`;
      return this.http.get<Users>(apiUrl);
    }

    postUsers(users: Users): Observable<any>
    {
      return this.http.post<Users>(this.contactsURL, users, this.httpOptions).pipe(tap(result => console.log(result)))
    }

    updateUsers(users: Users): Observable<any>
    {
      return this.http.put<Users>(this.contactsURL, users, this.httpOptions)
    }

    deleteUsers(id: number): Observable<any>
    {
      const apiURL = `${this.contactsURL}/${id}`;
      return this.http.delete<number>(apiURL, this.httpOptions)
    }
}
