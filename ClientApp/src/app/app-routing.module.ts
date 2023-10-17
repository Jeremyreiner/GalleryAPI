import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RepositorySearchComponent } from '../app/repository-search/repository-search.component';

const routes: Routes = [
  { path: 'search-repositories', component: RepositorySearchComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
