import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Router, ActivatedRoute} from '@angular/router';
import {RecipeService} from '../recipe.service';

@Component({
  selector: 'app-update-recipe',
  templateUrl: './update-recipe.component.html',
  styleUrls: ['./update-recipe.component.css']
})
export class UpdateRecipeComponent implements OnInit {

  updateForm: FormGroup;
  id: number;

  constructor(private router: Router,
              private recipeService: RecipeService,
              private formBuilder: FormBuilder,
              private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.updateForm = this.formBuilder.group({
      author: [''],
      name: [''],
      type: [''],
      recipe: [''],
    });
    this.recipeService.getRecipeById(this.id)
      .subscribe((data) => {
        this.updateForm.patchValue(data);
      });
  }

  onUpdate(): void {
    this.recipeService.updateRecipe(this.id, this.updateForm.value)
      .subscribe(() => {
        this.router.navigate(['/browse']);
      },
        error => {
          alert(error);
        });
  }
}
