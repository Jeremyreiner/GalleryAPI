import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  name: string = '';

  constructor(
    private api: ApiService,
    private router: Router) { }

  onSubmit() {
    if (this.name) {
      this.api.login(this.name).subscribe((loginSuccessful) => {
        if (loginSuccessful) {
          this.router.navigate(['search-repositories']);
        } else {
          console.log("Error in logging in.");

        }
      });
    }
  }
}