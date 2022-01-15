import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from 'src/Helper/constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client-app';

  constructor(private router: Router){}
  
  onLogout(){
    localStorage.removeItem(Constants.USER_KEY);
    
  }

  get isUserLogin(){
    const user = localStorage.getItem(Constants.USER_KEY);
    return user && user.length>0;
  }
}
