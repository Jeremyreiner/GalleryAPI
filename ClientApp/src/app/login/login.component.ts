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
    private router: Router){}

  onSubmit() {
    console.log(`clicked `, this.name);

    this.api.login(this.name).subscribe((token: string) => {
      console.log(`token to storage`, token);
      localStorage.setItem('authToken', token)
    })
    this.router.navigate(['search-repositories']);
}}