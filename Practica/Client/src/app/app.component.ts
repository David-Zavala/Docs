import { Component, OnInit } from '@angular/core';
import ApiService from './shared/api.service';
import User from './shared/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  Users: Array<User>

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getAll().subscribe(data => {
      this.Users = data;
    });
  }
}
