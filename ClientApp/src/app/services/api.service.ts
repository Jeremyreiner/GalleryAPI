import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, debounceTime, map, of, switchMap } from 'rxjs';
import { GitHubItem } from '../models/gitHubItems';
import { Result } from '../models/result';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private url = `https://localhost:5002/api`;
  private repositoryUrl = `${this.url}/Repositories`;
  private authUrl = `${this.url}/Authentication`;

  public viewGallery = new BehaviorSubject<boolean>(false);
  public searchTerm = new BehaviorSubject<string>('');

  public searchTerm$ = this.searchTerm.asObservable();
  public viewGallery$ = this.viewGallery.asObservable();

  constructor(private http: HttpClient) { }

  getHeader() {
    const token = localStorage.getItem('authToken');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  storeToken(token: string) {
    localStorage.setItem('authToken', token);
  }

  getRepositories(): Observable<GitHubItem[]> {
    this.getHeader();
    return this.searchTerm$.pipe(
      debounceTime(300),
      switchMap((search) => {
        if (!search) {
          return of([]);
        }
        const url = `${this.repositoryUrl}/search/${search}`;
        return this.http.get<Result<GitHubItem[]>>(url, { headers: this.getHeader() })
          .pipe(
            map((data) => data.value)
          )
      }))
  }

  getUserGallery(): Observable<GitHubItem[]> {
    return this.viewGallery$.pipe(
      debounceTime(0),
      switchMap(() => {
        console.log('getting gallery');

        const url = `${this.repositoryUrl}/GetUserGallery`;
        return this.http.get<Result<GitHubItem[]>>(url, { headers: this.getHeader() })
          .pipe(
            map((data) => data.value)
          )
      }))
  }

  UpdateGallery(item: GitHubItem) {
    const url = `${this.repositoryUrl}/UpdateGallery`;
    this.http.post(url, item, { headers: this.getHeader() }).subscribe();

    this.getUserGallery()
  }

  login(name: string): Observable<boolean> {
    console.log('Attempting login');
    const url = `${this.authUrl}/Login/${name}`;

    return this.http.get<Result<string>>(url)
      .pipe(
        map((res) => {
          if (res.value) {
            this.storeToken(res.value);
            return true;
          }
          return false;
        }),
        catchError((error) => {
          console.log(`error: `, error);
          return [];
        })
      );
  }
}
