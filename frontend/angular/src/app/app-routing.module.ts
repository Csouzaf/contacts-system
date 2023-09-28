import { Component, NgModule, inject } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './components/contacts/contacts.component';
import { HomeComponent } from './components/home/home.component';
import { CreateContactsComponent } from './components/contacts/create-contacts/create-contacts.component';
import { EditContactsComponent } from './components/contacts/edit-contacts/edit-contacts.component';
import { RemoveContactsComponent } from './components/contacts/remove-contacts/remove-contacts.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { AuthGuard } from '@auth0/auth0-angular';

const routes: Routes = [

  {path: 'contacts', component: ContactsComponent},
  {path: 'home', component: HomeComponent},
  {path:'', redirectTo: '/login', pathMatch: 'full'},
  {path:'contacts/new', component: CreateContactsComponent},
  {path:'contacts/edit/:id',component:EditContactsComponent},
  {path:'contacts/remove/:id', component:RemoveContactsComponent},
  {path:'login',component:LoginComponent},
  {path:'signup',component:SignupComponent}
]
//canActivate: [() => inject(AuthGuard)]

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [
    RouterModule,

  ]
})
export class AppRoutingModule { }
