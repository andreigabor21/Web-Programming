import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {RecipeService} from '../recipe.service';
import {FormBuilder, FormGroup} from '@angular/forms';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.css']
})
export class AddRecipeComponent implements OnInit {

  addForm: FormGroup;

  constructor(private router: Router,
              private recipeService: RecipeService,
              private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.addForm = this.formBuilder.group({
      author: [''],
      name: [''],
      type: [''],
      recipe: [''],
    });
  }

  onSubmit(): void {
    this.recipeService.addRecipe(this.addForm.value)
      .subscribe(recipe => {
      this.router.navigate(['']);
       });
  }



}
