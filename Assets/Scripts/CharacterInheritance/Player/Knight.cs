using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Player
{
    [Header("KNIGHT ATTRIBUTES")]
    private bool canDash = true;
    private bool isDashing;
    private GameObject player;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCD = 1f;
    public TrailRenderer trailRenderer;

    private void Start()
    {
        player = this.gameObject;
        Physics2D.IgnoreLayerCollision(8, 7, true);
        Physics2D.IgnoreLayerCollision(8, 6, true);
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        } 
        PlayerFunctions();
        Dash();
    }

    protected override void PlayerFunctions()
    {
        base.PlayerFunctions();
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(dash());
        }
    }

    private IEnumerator dash() //PROVISIONAL PUEDO DASHEAR A TRAVES DE PAREDES
    {
        canDash = false;
        isDashing = true;
        player.gameObject.layer = 8;
        rb.velocity = new Vector2(playerInput.x * dashingPower, playerInput.y * dashingPower);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        boxCollider.enabled = true;
        trailRenderer.emitting = false;
        isDashing = false;
        player.gameObject.layer = 0;
        yield return new WaitForSeconds(dashingCD);
        canDash = true;
    }
}
