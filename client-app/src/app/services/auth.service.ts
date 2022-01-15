import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly baseURL: string = "http://localhost:16087/api/auth/"
  constructor(private httpClient: HttpClient) { }

  public login(email:string, password:string){
    const body = {
      Email: email,
      Password: password
    }

    return this.httpClient.post<ResponseModel>(this.baseURL + "login", body);
  }

  public register(fullName:string, email:string, password:string){
    const body = {
      fullName: fullName,
      Email: email,
      Password: password
    }

    return this.httpClient.post<ResponseModel>(this.baseURL + "register", body);
  }
}
