import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';

@Component({
  selector: 'app-comment-form',
  standalone: true,
  imports: [ReactiveFormsModule, NzButtonModule, NzInputModule],
  templateUrl: './comment-form.component.html',
  styleUrl: './comment-form.component.scss'
})
export class CommentFormComponent {
  private readonly fb = inject(FormBuilder);

  @Input() submitting = false;
  @Output() commentSubmit = new EventEmitter<string>();

  readonly form = this.fb.nonNullable.group({
    message: ['', [Validators.required]]
  });

  submit(): void {
    const message = this.form.controls.message.value.trim();

    if (!message || this.form.invalid || this.submitting) {
      this.form.controls.message.markAsTouched();
      return;
    }

    this.commentSubmit.emit(message);
    this.form.reset();
  }
}
