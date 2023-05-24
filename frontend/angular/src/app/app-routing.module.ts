import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './components/contacts/contacts.component';
import { HomeComponent } from './components/home/home.component';
import { CreateContactsComponent } from './components/contacts/create-contacts/create-contacts.component';
import { EditContactsComponent } from './components/contacts/edit-contacts/edit-contacts.component';
import { RemoveContactsComponent } from './components/contacts/remove-contacts/remove-contacts.component';

const routes: Routes = [
  
  {path: 'contacts', component: ContactsComponent},
  {path: 'home', component: HomeComponent},
  {path:'', redirectTo: '/home', pathMatch: 'full'},
  {path:'contacts/new', component: CreateContactsComponent},
  {path:'contacts/edit',component:EditContactsComponent},
  {path:'contacts/remove', component:RemoveContactsComponent}
]
@NgModule({
  imports: [
    RouterModule.forRoot(routes),

  ],
  exports: [
    RouterModule,

  ]
})
export class AppRoutingModule { }
