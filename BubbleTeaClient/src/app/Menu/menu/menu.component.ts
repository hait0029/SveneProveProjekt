import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/Category';  // Import the Category model

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    RouterModule,
    FormsModule,
    CommonModule // Required for ngIf, ngFor, etc.
  ],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  title = 'Menu'; // Updated title
  Categories: Category[] = [];  // Correct type for Categories


  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    console.log("Menu component initialized");
    this.categoryService.getAll().subscribe(x => {
      this.Categories = x;
    });
  }

  
}
