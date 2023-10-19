import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, debounceTime, map, of, switchMap, throwError } from 'rxjs';
import { GitHubData, GitHubItem } from '../models/gitHubItems';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private hardCoded = `https://localhost:5002/api`;
  private repositoryUrl = `${this.hardCoded}/Repositories`;
  private authUrl = `${this.hardCoded}/Authentication`;
  
  public viewGallery = new BehaviorSubject<boolean>(false);
  public searchTerm = new BehaviorSubject<string>('');
  
  public searchTerm$ = this.searchTerm.asObservable();
  public viewGallery$ = this.viewGallery.asObservable();

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
    return this.viewGallery$.pipe(
      debounceTime(0),
      switchMap(() => {
        const url = `${this.repositoryUrl}/GetUserGallery`;
        return this.http.get<GitHubItem[]>(url);
      }))
  }

  UpdateGallery(item: GitHubItem) {
    const url = `${this.repositoryUrl}/UpdateGallery`;
    return this.http.post(url, item).subscribe();
  }

  login(name: string): Observable<any> {
    console.log('attempting login');
    let url = `${this.authUrl}/Login/${name}`;
    return this.http.get<string>(url);
  }
}
