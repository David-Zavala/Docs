import { Component, OnInit } from '@angular/core';
import ApiService from './_services/api.service';
import User from './_data/User';

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
