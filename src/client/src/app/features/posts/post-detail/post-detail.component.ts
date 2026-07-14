import { DatePipe } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { finalize } from 'rxjs';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSpinModule } from 'ng-zorro-antd/spin';

import { Comment } from '../../../core/models/comment.model';
import { Post } from '../../../core/models/post.model';
import { PostService } from '../../../core/services/post.service';
import { CommentFormComponent } from '../comment-form/comment-form.component';
import { CommentListComponent } from '../comment-list/comment-list.component';

@Component({
  selector: 'app-post-detail',
  standalone: true,
  imports: [
    DatePipe,
    NzAvatarModule,
    NzCardModule,
    NzSpinModule,
    CommentFormComponent,
    CommentListComponent
  ],
  templateUrl: './post-detail.component.html',
  styleUrl: './post-detail.component.scss'
})
export class PostDetailComponent implements OnInit {
  private readonly postService = inject(PostService);

  post: Post | null = null;
  loading = true;
  submitting = false;

  ngOnInit(): void {
    this.postService
      .getPost()
      .pipe(finalize(() => (this.loading = false)))
      .subscribe((post) => {
        this.post = post;
      });
  }

  addComment(message: string): void {
    const currentPost = this.post;

    if (!currentPost) {
      return;
    }

    this.submitting = true;

    this.postService
      .addComment(message)
      .pipe(finalize(() => (this.submitting = false)))
      .subscribe((comment: Comment) => {
        this.post = {
          ...currentPost,
          comments: [...currentPost.comments, comment]
        };
      });
  }
}
