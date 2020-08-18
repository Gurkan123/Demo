import { Component, OnInit } from '@angular/core';
import { RoleService } from '../_services/role.service';
import { Perm } from '../_models/perm';

@Component({
  selector: 'app-perm',
  templateUrl: './perm.component.html',
  styleUrls: ['./perm.component.css']
})
export class PermComponent implements OnInit {
  perms: Perm[];
  model: any = {};

  constructor(private roleService: RoleService) { }

  ngOnInit() {
    this.loadPerm();
  }

  loadPerm() {
    this.roleService.getPerms().subscribe((perms: Perm[]) => {
      this.perms = perms;
    }, error => {
      console.log('Error');
    });
  }

  deletePerm(id: number) {
    this.roleService.deletePerm(id).subscribe(() => {
      this.loadPerm();
    });
  }

  postPerm() {
    this.roleService.postPerm(this.model).subscribe((data) => {
      console.log(data);
      this.loadPerm();
    }, error => {
      console.log('Error post');
    });
  }

}
