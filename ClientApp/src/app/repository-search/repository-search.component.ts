import { Component } from '@angular/core';
import { ApiService } from '../services/api.service'; // Import the ApiService
import { GitHubItem } from '../models/gitHubItems';

@Component({
  selector: 'app-repository-search',
  templateUrl: './repository-search.component.html',
  styleUrls: ['./repository-search.component.css'],
})
export class RepositorySearchComponent {
  searchTerm: string = '';
  searchResults: GitHubItem[] = [];

  constructor(private apiService: ApiService) {} // Inject the ApiService

  // onSubmit() {
  //   if (this.searchTerm !== '') {
  //     this.apiService.getRepositories(this.searchTerm).subscribe((data) => {
  //       this.searchResults = data; // Save the search results
  //       console.log('request, ', this.searchResults)
  //     });
  //   }
  // }
  onSubmit() {
    if (this.searchTerm) {
      this.apiService.getRepositories(this.searchTerm).subscribe(
        (response) => {
          console.log(`type, `, typeof(response))
          if (response.items && Array.isArray(response.items)) {
            this.searchResults = response.items.map((item: { full_name: string; owner: { avatar_url: any; }; }) => {
              const mappedItem = new GitHubItem();
              mappedItem.full_name = item.full_name;

              if (item.owner) {
                mappedItem.owner = {
                  avatar_url: item.owner.avatar_url,
                };
              }

              return mappedItem;
            });
          } else {
            console.error('Invalid data format received from the API:', response);
          }
        },
        (error) => {
          console.error('Error fetching data from the API:', error);
        }
      );
    }
  }
}
// Save to grepper
// This code checks if data is an array using Array.isArray(data) before attempting to use the map method. Additionally, it includes error handling for both the data format and any errors that may occur during the HTTP request. Make sure to import your GitHubItem model as shown above.