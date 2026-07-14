import { DatePipe } from '@angular/common';
import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { Subscription, forkJoin, finalize, interval, switchMap } from 'rxjs';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSpinModule } from 'ng-zorro-antd/spin';

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
export class PostDetailComponent implements OnInit, OnDestroy {
  private readonly postService = inject(PostService);
  private commentRefreshSubscription?: Subscription;
  private readonly commentRefreshMs = 5000;

  posts: Post[] = [];
  loading = true;
  submittingPostIds = new Set<number>();

  ngOnInit(): void {
    this.postService
      .getPosts()
      .pipe(finalize(() => (this.loading = false)))
      .subscribe((posts) => {
        this.posts = posts;
        this.startCommentRefresh();
      });
  }

  ngOnDestroy(): void {
    this.commentRefreshSubscription?.unsubscribe();
  }

  addComment(post: Post, message: string): void {
    this.submittingPostIds = new Set(this.submittingPostIds).add(post.id);

    this.postService
      .addComment(post.id, message)
      .pipe(
        finalize(() => {
          const nextSubmittingPostIds = new Set(this.submittingPostIds);
          nextSubmittingPostIds.delete(post.id);
          this.submittingPostIds = nextSubmittingPostIds;
        })
      )
      .subscribe((comments) => {
        this.posts = this.posts.map((currentPost) =>
          currentPost.id === post.id
            ? {
                ...currentPost,
                comments
              }
            : currentPost
        );
      });
  }

  isSubmitting(postId: number): boolean {
    return this.submittingPostIds.has(postId);
  }

  private startCommentRefresh(): void {
    this.commentRefreshSubscription?.unsubscribe();

    if (this.posts.length === 0) {
      return;
    }

    this.commentRefreshSubscription = interval(this.commentRefreshMs)
      .pipe(switchMap(() => forkJoin(this.posts.map((post) => this.postService.getComments(post.id)))))
      .subscribe((commentsByPostIndex) => {
        this.posts = this.posts.map((post, index) => ({
          ...post,
          comments: commentsByPostIndex[index] ?? post.comments
        }));
      });
  }
}
