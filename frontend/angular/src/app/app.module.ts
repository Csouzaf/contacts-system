import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavComponent } from './components/shared/nav/nav.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/shared/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { ContactsComponent } from './components/contacts/contacts.component';


import { EditContactsComponent } from './components/contacts/edit-contacts/edit-contacts.component';
import { RemoveContactsComponent } from './components/contacts/remove-contacts/remove-contacts.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateContactsComponent } from './components/contacts/create-contacts/create-contacts.component';
import { UserSharedService } from './components/services/user-shared.service';
import { ContactsService } from './components/services/contacts.service';

import { SignupComponent } from './components/signup/signup.component';
import { LoginComponent } from './components/login/login.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    ContactsComponent,
    NavComponent,
    FooterComponent,
    HeaderComponent,
    HomeComponent,
    CreateContactsComponent,
    EditContactsComponent,
    RemoveContactsComponent,
    SignupComponent,
    LoginComponent,


  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

  ],


  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi:true
  }],



    bootstrap: [AppComponent]
})
export class AppModule { }
