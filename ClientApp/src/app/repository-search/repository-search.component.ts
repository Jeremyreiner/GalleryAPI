import { Component } from '@angular/core';
import { ApiService } from '../services/api.service'; // Import the ApiService
import { GitHubItem } from '../models/gitHubItems';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-repository-search',
  templateUrl: './repository-search.component.html',
  styleUrls: ['./repository-search.component.css'],
})
export class RepositorySearchComponent{
  searchTerm: string = '';
  userGallery: GitHubItem[] = [];
  openGallery: boolean = false;

  items$: Observable<GitHubItem[]> = this.apiService.getRepositories();

  constructor(
    private apiService: ApiService) { } // Inject the ApiService

  bookmarkItem(item: GitHubItem) {
    this.apiService.UpdateGallery(item);
    // Make API call that adds item to db
    //Reorder final list
    this.updateGalery();
  }

  toggleGallery(){
    this.openGallery = !this.openGallery;

    if(this.openGallery === true){
      this.updateGalery();
    }
  }

  updateGalery(){
    this.apiService.getUserGallery().subscribe((res) => {
      this.userGallery = res;
    });
  }

  onSubmit() {
    this.apiService.searchTerm.next(this.searchTerm);
}}