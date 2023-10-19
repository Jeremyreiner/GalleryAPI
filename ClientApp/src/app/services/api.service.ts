import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { BehaviorSubject, Observable,debounceTime, map, of, switchMap } from 'rxjs';
import { GitHubData, GitHubItem } from '../models/gitHubItems';
import { TokenModel } from '../models/TokenModel';

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

  getHeader(){
    const token = localStorage.getItem('authToken');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  storeToken(token: string){
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
        return this.http.get<GitHubData>(url, { headers: this.getHeader()})
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
        return this.http.get<GitHubItem[]>(url, { headers: this.getHeader()});
      }))
  }

  UpdateGallery(item: GitHubItem) {
    const url = `${this.repositoryUrl}/UpdateGallery`;
    return this.http.post(url, item, { headers: this.getHeader()}).subscribe();
  }

  login(name: string): Observable<boolean> {
    console.log('Attempting login');
    const url = `${this.authUrl}/Login/${name}`;
  
    return new Observable<boolean>((observer) => {
      this.http.get<TokenModel>(url).subscribe(
        (token: TokenModel) => {
          this.storeToken(token.token);
          observer.next(true); // Notify success
          observer.complete();
        },
        (error) => {
          console.log(`error: `, error);
          observer.next(false); // Notify failure
          observer.complete();
        }
      );
    });
  }
  
}
