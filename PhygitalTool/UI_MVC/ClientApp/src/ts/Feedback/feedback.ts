import {createReaction, readReactions, createLikePost, createDislikePost, deleteReaction} from "./feedbackRest";

let posts = document.querySelectorAll('.post');
const likeButtons = document.querySelectorAll('.bi-hand-thumbs-up');
const dislikeButtons = document.querySelectorAll('.bi-hand-thumbs-down');
 async function getReactionsOfPost(postId: number){
    await readReactions(postId)
        .then(reactions => {
            let bodyData =``;
            reactions.forEach(reaction => {
                bodyData += `
                <div class="row d-flex spacing-top form-control">
                    <div class="input-group gap-2">
                        <div class="col-2 d-flex justify-content-start">
                            <p class="form-control">${reaction.accountName}</p>
                        </div>
                        <div class="col-8 d-flex justify-content-between">
                            <p class="form-control">${reaction.content}</p>
                        </div>
                        <div class="col ">
                            <div class="d-flex justify-content-end">
                                <button class="btn btn-danger bi bi-trash" id="${reaction.id}"></button>
                            </div>
                        </div>
                    </div>
                </div>`;
            })
           
            let reactionList = document.getElementById(`listReactions_${postId}`) as HTMLElement;
            if(reactionList){
                reactionList.innerHTML = bodyData;
            }else{
                console.log(`reactionList for post ${postId} is null`);
            }
            removeReaction(postId, reactions);
        })
        .catch(error => {
            console.error(error);
        });

}

 async function getReactions(){
    posts.forEach(post => {
        let postId = post.getAttribute('data-post-id');
        if (postId !== null){
            getReactionsOfPost(parseInt(postId));
        }
    })
}


 async function sendReaction( postId: number) {
     let content = (document.querySelector(`input[name="content_${postId}"]`) as HTMLInputElement);
    const reactionObject: Reaction = {
        content: content.value
    }
    console.log(reactionObject);
    
    await createReaction(postId, reactionObject)
        .then(reaction => {
            console.log(reaction);
            
            let reactionCountElement = document.getElementById(`reactionCount_${postId}`);
            if(reactionCountElement !== null){
                let currentCount = parseInt(reactionCountElement.innerText);
                console.log(currentCount);
                reactionCountElement.innerText = (currentCount + 1).toString();
            }
           
        })
        .catch(error => {
            if(error.status === 400) {
                alert(error.message.errors.Content[0]);
            }else{
                console.error(error);
            }
        });
   content.value = '';
}

async function removeReaction(postId: number, reactions: ReactionRead[]){
  reactions.forEach(reaction => {
      const deleteButton = document.getElementById(`${reaction.id}`) as HTMLButtonElement;
      if(deleteButton){
          deleteButton.addEventListener('click', async (event) => {
              event.preventDefault();
              await deleteReaction(postId, reaction.id)
                  .then(() => {
                    console.log(`Reaction ${reaction.id} deleted`);
                    //todo: update reaction count in UI realtime => shouldn't go under 0
                      let reactionCountElement = document.getElementById(`reactionCount_${postId}`);
                      if(reactionCountElement !== null){
                          let currentCount = parseInt(reactionCountElement.innerText);
                          console.log(currentCount);
                          reactionCountElement.innerText = (currentCount - 1).toString();
                      }
                      getReactionsOfPost(postId);
                  })
                  .catch(error => {
                      if (error.status === 403) {
                          alert('Je mag niet andermans reacties verwijderen');
                      }else{
                          console.error(error);
                      }
                  });
          });
      }
  });
}

async function handlePostLikes(){
     likeButtons.forEach(button => {
        button.addEventListener('click', async (event) => {
            const postId = button.getAttribute('data-post-id');
            event.preventDefault();
            if (postId !== null){
                (button as HTMLButtonElement).disabled = true;
                 await createLikePost(parseInt(postId))
                    .then(result => {
                    console.log(`Post ${postId} liked`);

                        if (typeof result === 'object' && result !== null){
                            const {likeCount, dislikeCount} = result;
                            const likeCountElement = document.getElementById(`likeCount_${postId}`);
                            const dislikeCountElement = document.getElementById(`dislikeCount_${postId}`);
                            if (likeCountElement !== null && dislikeCountElement !== null) {
                                likeCountElement.innerText = likeCount.toString();
                                dislikeCountElement.innerText = dislikeCount.toString();
                            }
                        }
                })
                    .catch(error => {
                        console.error(error);
                    });
                (button as HTMLButtonElement).disabled = false;
            }
        });
    
    });
}

async function handleReactionLikes(){
    const likeButtons = document.querySelectorAll('.reaction-like-button');
    likeButtons.forEach(button => {
        button.addEventListener('click', async (event) => {
            const reactionId = button.getAttribute('data-reaction-id');
            event.preventDefault();
            if (reactionId !== null){
                (button as HTMLButtonElement).disabled = true;
                await fetch(`/api/feedbacks/${reactionId}/LikeReaction`, {
                    method: 'POST'
                })
                .then(response => response.json())
                .then(result => {
                    console.log(`Reaction ${reactionId} liked`);
                    const { likeCount, dislikeCount } = result;
                    const likeCountElement = document.getElementById(`likeCount_${reactionId}`);
                    const dislikeCountElement = document.getElementById(`dislikeCount_${reactionId}`);
                    if (likeCountElement !== null && dislikeCountElement !== null) {
                        likeCountElement.innerText = likeCount.toString();
                        dislikeCountElement.innerText = dislikeCount.toString();
                    }
                })
                .catch(error => console.error(error));
                (button as HTMLButtonElement).disabled = false;
            }
        });
    });
}


async function handlePostDislikes(){
    dislikeButtons.forEach(button => {
        button.addEventListener('click', async (event) => {
            const postId = button.getAttribute('data-post-id');
            event.preventDefault();
            if (postId !== null){
                (button as HTMLButtonElement).disabled = true;
                await createDislikePost(parseInt(postId))
                    .then(result => {
                    console.log(`Post ${postId} disliked`);

                    if (typeof result === 'object' && result !== null){
                        const { dislikeCount, likeCount} = result;
                         const likeCountElement = document.getElementById(`likeCount_${postId}`);
                        const dislikeCountElement = document.getElementById(`dislikeCount_${postId}`);
                        if ( dislikeCountElement !== null && likeCountElement !== null) {
                             likeCountElement.innerText = likeCount.toString();
                            dislikeCountElement.innerText = dislikeCount.toString();
                        }
                    }
                    
                }).catch(error => {
                        console.error(error);
                    });
                (button as HTMLButtonElement).disabled = false;
            }
        });
    
    });

}

getReactions();
handlePostLikes();
handlePostDislikes();

 posts.forEach(post => {
    let postId = post.getAttribute('data-post-id');
    if (postId !== null){
        let reactionForm = document.getElementById(`reactionForm_${postId}`) as HTMLFormElement;
        reactionForm.addEventListener('submit', async (event) => {
            event.preventDefault();
            if (postId !== null){
                await sendReaction(parseInt(postId));
                await getReactionsOfPost(parseInt(postId));
            }
        });
    }
    
 });

