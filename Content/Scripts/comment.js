
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
      type: 'html',
      method: 'post',
      data: collection,
      success: function(html)
      {
        var button = document.querySelector('#blogNewComment .button');
        id('blogNewComment').innerHTML = html;
        
        if(button)
        {
          button.addEventListener('click', postComment, false);
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