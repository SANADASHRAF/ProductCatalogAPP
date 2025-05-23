import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ServiceResponse } from '../../Models/service-response';
import { UserDto } from '../../Models/user-dto';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  login() {

    if (!this.email || !this.password) {
      this.errorMessage = 'يرجى إدخال البريد الإلكتروني وكلمة المرور';
      return;
    }

    console.log('Sending login data:', { email: this.email, password: this.password });

    this.authService.login(this.email, this.password).subscribe({
      next: (response: ServiceResponse<UserDto>) => {
        console.log('Login response:', response);
        if (response.success) {
          localStorage.setItem('token', response.data.token);
          this.router.navigate(['/products']);
        } else {
          this.errorMessage = response.message;
        }
      },
      error: (err) => {
        console.error('Login error:', err);
        if (err.error && err.error.message) {
          this.errorMessage = err.error.message;
        } else {
          this.errorMessage = 'حدث خطأ أثناء تسجيل الدخول';
        }
      }
    });
  }
}