import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RepositorySearchComponent } from '../app/repository-search/repository-search.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: 'search-repositories', component: RepositorySearchComponent },
  { path: 'login', component: LoginComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
