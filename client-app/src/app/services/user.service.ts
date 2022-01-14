import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseURL: string = "http://localhost:16087/api/user/"
  constructor(private httpClient: HttpClient) { }

  public userList(){
    const header = new HttpHeaders({
      'Authorization': `Bearer ${JSON.parse(JSON.stringify(localStorage.getItem("userInfo"))).token}`
    })
    return this.httpClient.get(this.baseURL + "userlist", {headers: header} );
  }
}
