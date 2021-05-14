import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Recipe} from './recipe';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  constructor(private http: HttpClient) { }

  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>('http://localhost/lab7php/list.php');
  }

  addRecipe(recipe: Recipe): Observable<any> {
    console.log(recipe);
    return this.http.post('http://localhost/lab7php/add.php', recipe);
  }

  deleteRecipe(id: number): Observable<any> {
    return this.http.delete( 'http://localhost/lab7php/delete.php?id=' + id);
  }

  getRecipeById(id: number): any {
    return this.http.get<Recipe[]>( 'http://localhost/lab7php/get-by.php?id=' + id);
  }

  updateRecipe(id: number, recipe: Recipe): any {
    return this.http.put('http://localhost/lab7php/update.php' + '?id=' + id, recipe);
  }
}
