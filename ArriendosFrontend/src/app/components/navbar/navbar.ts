import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  imports: [RouterLink],
  selector: 'app-navbar',
  standalone:true,
  templateUrl: './navbar.html',
})
export class Navbar {}
