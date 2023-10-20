import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '../services/api.service'; // Import the ApiService
import { GitHubItem } from '../models/gitHubItems';
import { Observable } from 'rxjs';
import { MatSelectionListChange } from '@angular/material/list';

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

    this.items$.subscribe((res) => {
      this.hasResults = res.length > 0;
    });
  }

  bookmarkItem(item: MatSelectionListChange) {
    this.apiService.UpdateGallery(item.options[0].value);
    this.apiService.viewGallery.next(this.openGallery);
  }

  toggleGallery() {
    this.openGallery = !this.openGallery;
  }

  onSubmit() {
    this.apiService.searchTerm.next(this.searchTerm);
  }
}