import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm = this.formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required]
  })
  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(){
    console.log("on submit");
    let email = this.loginForm.controls["email"].value;
    let password = this.loginForm.controls["password"].value;

    this.authService.login(email, password).subscribe((data:any) => {
      if(data.ResponseCode == 1){
          localStorage.setItem("userInfo", JSON.stringify(data.DataSet));
          this.router.navigate(["/user-management"])
      }
      console.log("response", data)
    }, error => {
      console.log("error", error)
    })
  }
}
