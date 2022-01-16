import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from '../models/responseModel';
import { map } from 'rxjs/operators';
import { ResponseCode } from '../enums/responseCode';
import { User } from '../models/user';
import { Constants } from 'src/Helper/constants';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseURL: string = "http://localhost:16087/api/user/"
  constructor(private httpClient: HttpClient) { }

  public userList(){
    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const header = new HttpHeaders({
      'Authorization': `Bearer ${ userInfo?.token }`
    })


    return this.httpClient.get<ResponseModel>(this.baseURL + "userlist", {headers: header} ).pipe(map(res=>{
      let userList = new Array<User>();
      if(res.responseCode == ResponseCode.Ok){
        if(res.dataSet){
          res.dataSet.map((x:User) => {
            userList.push(new User(x.fullName, x.email, x.userName))
          })
        }
      }
     return userList;
      
    }));
  }
}
