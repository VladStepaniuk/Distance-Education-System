import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Role } from '../models/role';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public roles: Role[] = [];
  public registerForm = this.formBuilder.group({
    fullName: ['', [Validators.required]],
    email: ['', [Validators.email, Validators.required]],
    password: ['', Validators.required]
  })
  constructor(private formBuilder: FormBuilder, private authService:AuthService, private userService: UserService) { }

  ngOnInit(): void {
    this.getAllRoles();
  }

  onSubmit(){
    console.log("on submit");
    let fullName = this.registerForm.controls["fullName"].value;
    let email = this.registerForm.controls["email"].value;
    let password = this.registerForm.controls["password"].value;

    this.authService.register(fullName, email, password).subscribe((data) => {
      console.log("response", data)
    }, error => {
      console.log("error", error)
    })
  }

  getAllRoles(){
    this.userService.getAllRole().subscribe(roles => {
      this.roles = roles;
    });
  }
}
