import { Component, OnInit } from '@angular/core';
import {Recipe} from '../recipe';
import {RecipeService} from '../recipe.service';

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrls: ['./browse.component.css']
})
export class BrowseComponent implements OnInit {

  recipes: Recipe[] = [];
  searchType: string;

  constructor(private recipeService: RecipeService) { }

  ngOnInit(): void {
    this.recipeService.getRecipes()
      .subscribe(recipes => this.recipes = recipes);
  }

  onDeleteClicked(id: number): void {
    this.recipeService.deleteRecipe(id)
      .subscribe(() => {
        this.recipes = this.recipes.filter(
          listing => listing.id !== id
        );
      });
  }

  search(): void {
    if (this.searchType === '') {
      this.ngOnInit();
    }
    else {
      this.recipes = this.recipes.filter(res => {
        return res.type.toLowerCase().match(this.searchType.toLowerCase());
      });
    }
  }

}
