import { Injectable } from '@angular/core';
import { Observable, delay, of } from 'rxjs';

import { Comment } from '../models/comment.model';
import { Post } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private readonly mockPost: Post = {
    id: 1,
    title: 'IT 08-1',
    authorName: 'Chang-noi',
    authorAvatarUrl: 'https://i.pravatar.cc/80?img=12',
    createdAt: '2026-07-14T09:42:00+07:00',
    imageUrl:
      'https://images.unsplash.com/photo-1601758124510-52d02ddb7cbd?auto=format&fit=crop&w=1100&q=80',
    comments: [
      {
        id: 1,
        authorName: 'dfasdf 365',
        authorAvatarUrl: 'https://i.pravatar.cc/80?img=5',
        message: 'Comment',
        createdAt: '2026-07-14T10:03:00+07:00'
      },
      {
        id: 2,
        authorName: 'dfasdf 365',
        authorAvatarUrl: 'https://i.pravatar.cc/80?img=8',
        message: 'Test again day',
        createdAt: '2026-07-14T10:15:00+07:00'
      }
    ]
  };

  getPost(): Observable<Post> {
    return of(this.clonePost()).pipe(delay(350));
  }

  addComment(message: string): Observable<Comment> {
    const comment: Comment = {
      id: Date.now(),
      authorName: 'Current User',
      authorAvatarUrl: 'https://i.pravatar.cc/80?img=20',
      message,
      createdAt: new Date().toISOString()
    };

    this.mockPost.comments = [...this.mockPost.comments, comment];

    return of(comment).pipe(delay(300));
  }

  private clonePost(): Post {
    return {
      ...this.mockPost,
      comments: this.mockPost.comments.map((comment) => ({ ...comment }))
    };
  }
}
