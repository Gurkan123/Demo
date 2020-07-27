import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: User[];
  model: any;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getUsers().subscribe((users: User[]) => {
      this.users = users;
    }, error => {
      console.log('Error');
    });
  }

  updateUser(id: number) {
    this.userService.updateUser(id, this.model).subscribe(() => {
      this.loadUser();
    });
  }

}
