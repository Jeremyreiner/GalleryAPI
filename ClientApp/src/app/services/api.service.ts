import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, debounceTime, map, of, switchMap, throwError } from 'rxjs';
import { GitHubData, GitHubItem } from '../models/gitHubItems';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private hardCoded = `https://localhost:5002/api`;
  private repositoryUrl = `${this.hardCoded}/Repositories`
  private authUrl = `${this.hardCoded}/Authentication`
  public searchTerm = new BehaviorSubject<string>('');
  public searchTerm$ = this.searchTerm.asObservable();
  
  constructor(private http: HttpClient) { }

  // Example method for making a request to your .NET Web API
  getRepositories(): Observable<GitHubItem[]> {
    return this.searchTerm$.pipe(
      debounceTime(300),
      switchMap((search) => {
        if (!search) {
          return of([]);
        }
        const url = `${this.repositoryUrl}/search/${search}`;
        return this.http.get<GitHubData>(url)
          .pipe(
            map((data) => data.items)
          )
      }))
  }

  getUserGallery(): Observable<GitHubItem[]> {
    const url = `${this.repositoryUrl}/GetUserGallery`;
    console.log(`updating gallery`);
    return this.http.get<GitHubItem[]>(url);
  }

  UpdateGallery(item: GitHubItem){
    ///Repositories/UpdateGallery
    const url = `${this.repositoryUrl}/UpdateGallery`;
    console.log(`inserting item: `, item);
    this.http.post(url, item).subscribe();
  }

login(): Observable <any> {
  console.log('attempting login');
  let name = 'Jimmy';
  let url = `${this.authUrl}/Login/${name}`;
  return this.http.get<string>(url);
}
  // You can add more methods for various API interactions here
}
