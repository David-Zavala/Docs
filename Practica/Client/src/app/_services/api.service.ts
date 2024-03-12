import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import User from '../_data/User';

@Injectable()
export default class ApiService {
  public API = "https://localhost:44335/";
  public USERS_ENDPOINT = `${this.API}Users/GetUsers`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Array<User>> {
    return this.http.get<Array<User>>(this.USERS_ENDPOINT);
  }
}
