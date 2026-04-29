using UnityEngine;

public class RobotSmallIdle : IStates
{
    RobotSmall player;

    public RobotSmallIdle(RobotSmall player) { this.player = player; }

    public void Enter() { }
    public void Update() { }
    public void Exit() { }

    public string GetName() { return "Idle"; }

}

public class RobotSmallShoot : IStates
{
    RobotSmall player;
    Vector3 directionMagnet;

    public RobotSmallShoot(RobotSmall player) { this.player = player; }

    public void Enter() 
    {
        SearchTarget();
        player.isEnergized = false;
    }
    public void Update() 
    {
        Shoot();
        float distanceToPlayer = Vector3.Distance(player.magnet.gameObject.transform.localPosition, player.magnetStart);
        if (player.magnet.hasHooked())
        {
            player.ChangeState(new RobotSmallPullObj(player));
        }
        else
        {
            if (distanceToPlayer > player.magnet.maxDistance - 2)
            {
                player.ChangeState(new RobotSmallRetract(player));
            }
            if (player.magnet.hit)
            {
                player.ChangeState(new RobotSmallRetract(player));
            }
        }
    }
    public void Exit() { player.magnet.rb.linearVelocity = Vector3.zero; }

    void SearchTarget()
    {
        Ray rayTarget = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 vectorOffset = player.raycastOffset.position - rayTarget.origin;

        float offset = Vector3.Dot(vectorOffset, rayTarget.direction);
        rayTarget.origin = rayTarget.origin + (Camera.main.transform.forward * offset);

        if (Physics.Raycast(rayTarget, out RaycastHit hit, player.magnet.maxDistance))
        {
            directionMagnet = (hit.point - player.magnet.gameObject.transform.position).normalized;
            player.magnet.ShootMagnet();     
        }
        else
        {
            directionMagnet = rayTarget.direction;
        }
        if (player.magnet.colliding > 0)
        {
            directionMagnet = Vector3.zero;
        }
    }
    void Shoot()
    { 
        player.magnet.rb.linearVelocity = directionMagnet * player.magnet.magnetSpeed;
    }

    public string GetName() { return "Shoot"; }

}

public class RobotSmallPullObj : IStates
{
    RobotSmall player;

    public RobotSmallPullObj(RobotSmall player) { this.player = player; }

    public void Enter() { player.magnet.rb.linearVelocity = Vector3.zero; }
    public void Update() 
    {
        float distanceToPlayer = Vector3.Distance(player.magnet.gameObject.transform.localPosition, player.magnetStart);
        if (player.magnet.hasHooked())
            if (player.magnet.pullself)
                player.ChangeState(new RobotSmallPullSelf(player));

        if (distanceToPlayer < 1.5f)
            player.ChangeState(new RobotSmallRetract(player));
    }
    public void Exit() { }

    public string GetName() { return "PullObj"; }

}

public class RobotSmallPullSelf : IStates
{
    RobotSmall player;
    Vector3 direction;

    public RobotSmallPullSelf(RobotSmall player) { this.player = player; }

    public void Enter() 
    {
        player.gravity = 0;
        player.magnet.rb.linearVelocity = Vector3.zero;
    }
    public void Update()
    {
        float distanceToHook = Vector3.Distance(player.magnetStart, player.magnet.gameObject.transform.localPosition);
        
        direction = (player.magnet.gameObject.transform.position - player.transform.position).normalized;
        Vector3 move = direction * player.magnet.playerPullSpeed * Time.deltaTime;
        player.controller.Move(move);
        
        if (distanceToHook < 0.8f)
        {
            player.ChangeState(new RobotSmallRetract(player));
        }
    }
    public void Exit() 
    {
        player.gravity = player.selfGravity;
        player.directionFall = direction;
    }

    public string GetName() { return "PullSelf"; }

}

public class RobotSmallInertial : IStates
{
    RobotSmall player;

    public RobotSmallInertial(RobotSmall player) { this.player = player; }

    public void Enter() 
    {
        player.magnet.ReleaseHooked();
        player.magnet.rb.linearVelocity = Vector3.zero; 
    }
    public void Update()
    {
        if (player.controller.isGrounded)
        {
            player.ChangeState(new RobotSmallIdle(player));
        }

        Vector3 move = player.directionFall * player.magnet.playerPullSpeed/1.3f * Time.deltaTime;
        player.controller.Move(move);
        
    }
        
    public void Exit() { player.isEnergized = true; }

    public string GetName() { return "Inertial"; }

}

public class RobotSmallRetract : IStates
{
    RobotSmall player;

    public RobotSmallRetract(RobotSmall player) { this.player = player; }

    public void Enter() { player.magnet.ReleaseHooked(); }
    public void Update()
    {
        if (Vector3.Distance(player.magnet.gameObject.transform.localPosition, player.magnetStart) < 0.23f)
        {
            player.ChangeState(new RobotSmallIdle(player));
        }
    }
    public void Exit() 
    {
        player.magnet.transform.localPosition = player.magnetStart;
        player.magnet.rb.linearVelocity = Vector3.zero;
        player.isEnergized = true; 
    }

    public string GetName() { return "Retract"; }

}