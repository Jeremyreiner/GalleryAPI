import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service'; // Import the ApiService
import { GitHubItem } from '../models/gitHubItems';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-repository-search',
  templateUrl: './repository-search.component.html',
  styleUrls: ['./repository-search.component.css'],
})
export class RepositorySearchComponent {
  searchTerm: string = '';
  openGallery: boolean = false;
  hasResults: boolean = false;

  galleryItems$: Observable<GitHubItem[]> = this.apiService.getUserGallery(); //need a way to track changes immediatly 
  items$: Observable<GitHubItem[]> = this.apiService.getRepositories();

  constructor(
    private apiService: ApiService) { 
  
      this.items$.subscribe((itemsResults) => {
        this.hasResults = itemsResults.length > 0;
      });
    }

  bookmarkItem(item: GitHubItem) {
    this.apiService.UpdateGallery(item);
    this.viewGallery()
  }

  toggleGallery(){
    this.openGallery = !this.openGallery;
  }

  viewGallery(){
    this.apiService.viewGallery.next(this.openGallery);
  }

  onSubmit() {
    this.apiService.searchTerm.next(this.searchTerm);
}}