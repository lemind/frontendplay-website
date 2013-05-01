
// this file is for the comment functionality
// comment box is loaded async (to prevent spam)

(function()
{
  var link = id('blogNewComment').getAttribute('data-post');
  
  // initial request for comment form
  reqwest({
    url: link,
    type: 'html',
    success: function(html)
    {
      id('blogNewComment').innerHTML = html;
      document.querySelector('#blogNewComment .button').addEventListener('click', postComment, false);
    }
  });
  
  
  // posts a comment to server
  function postComment(e)
  {
    e.preventDefault();
    
    var collection = {
      Name: id('Name').value,
      Email: id('Email').value,
      Website: id('Website').value,
      Comment: id('Comment').value
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
          list.innerHTML = response.newComment + list.innerHTML;
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