import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { BrowseComponent } from './browse/browse.component';
import { RouterModule, Routes } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { MainComponent } from './main/main.component';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { UpdateRecipeComponent } from './update-recipe/update-recipe.component';

export const routes: Routes = [
  { path: '', component: MainComponent, pathMatch: 'full' },
  { path: 'browse', component: BrowseComponent },
  { path: 'add', component: AddRecipeComponent },
  { path: 'edit/:id', component: UpdateRecipeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    AddRecipeComponent,
    BrowseComponent,
    NavBarComponent,
    MainComponent,
    UpdateRecipeComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
