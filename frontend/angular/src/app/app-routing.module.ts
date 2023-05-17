import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './components/contacts/contacts.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  {path: 'contacts', component: ContactsComponent},
  {path: 'home', component: HomeComponent},
  {path:'', redirectTo: '/home', pathMatch: 'full'}
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
