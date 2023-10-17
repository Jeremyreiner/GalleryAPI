import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { GitHubItem } from '../models/gitHubItems';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiBaseUrl = '/api'; // Use the base URL defined in the proxy configuration
  private hardCoded = `https://localhost:5002/api/`

  constructor(private http: HttpClient) {}

  // Example method for making a request to your .NET Web API
  getRepositories(query: string): Observable<any> {
    const url = `${this.hardCoded}Repositories/search/${query}`;
    
    return this.http.get(url);
  }

  // You can add more methods for various API interactions here
}
