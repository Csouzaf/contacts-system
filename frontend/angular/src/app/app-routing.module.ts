import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './components/contacts/contacts.component';
import { HomeComponent } from './components/home/home.component';
import { CreateContactsComponent } from './components/contacts/create-contacts/create-contacts.component';

const routes: Routes = [
  {path: 'contacts', component: ContactsComponent},
  {path: 'home', component: HomeComponent},
  {path:'', redirectTo: '/home', pathMatch: 'full'},
  {path:'contacts/new', component: CreateContactsComponent}
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
