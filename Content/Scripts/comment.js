
// this file is for the comment functionality
// comment box is loaded async (to prevent spam)

(function()
{
  var link = id('blogNewComment').getAttribute('data-post');
  
  
  // initial request for comment form
  setTimeout(function()
  {
    reqwest({
      url: link,
      type: 'html',
      success: function(html)
      {
        id('blogNewComment').innerHTML = html;
        document.querySelector('#blogNewComment .button').addEventListener('click', postComment, false);
      }
    });
  }, 2000); // timeout to fuck off spam bots
  
  
  // posts a comment to server
  function postComment(e)
  {
    e.preventDefault();
    
    var collection = {
      Name: id('Name').value,
      Email: id('Email').value,
      Website: id('Website').value,
      Comment: id('Comment').value,
      Body: id('Body').value,
      '__RequestVerificationToken': document.querySelector('[name="__RequestVerificationToken"]').value
    };
    
    reqwest({
      url: link,
      type: 'json',
      method: 'post',
      data: collection,
      success: function(response)
      {
        id('blogNewComment').innerHTML = response.output;

        if(!response.success)
        {
          document.querySelector('#blogNewComment .button').addEventListener('click', postComment, false);
        }
        else
        {
          var list = document.querySelector('#blogComments ul');
          var empty = id('noComments');
          
          list.innerHTML = response.newComment + list.innerHTML;
          
          if(empty)
          {
            empty.parentNode.removeChild(empty);
          }
        }
      }
    });
  }
  
  
  // id selector
  function id(id)
  {
    return document.getElementById(id);
  };
})(); 