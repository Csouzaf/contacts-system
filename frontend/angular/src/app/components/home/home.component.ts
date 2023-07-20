import { AuthLoginService } from './../services/auth-login.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private authLoginService : AuthLoginService){}

  ngOnInit(): void {
    this.authLoginService.getUser().subscribe((result)=>{
     console.log(result)
    },
    error => {
      console.error(error)
    })
  }


}
