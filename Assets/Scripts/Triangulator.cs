using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script can be used to split a 2D polygon into triangles.
/// The algorithm supports concave polygons, but not polygons with holes, 
/// or multiple polygons at once.
/// Taken from <see cref="http://wiki.unity3d.com/index.php?title=Triangulator"/>
/// </summary>
public class Triangulator {
    private readonly List<Vector2> _mPoints;

    public Triangulator(IEnumerable<Vector2> points)
    {
        _mPoints = new List<Vector2>(points);
    }

    public int[] Triangulate()
    {
        var indices = new List<int>();

        var n = _mPoints.Count;
        if (n < 3)
            return indices.ToArray();

        var V = new int[n];
        if (Area() > 0) {
            for (var v = 0; v < n; v++)
                V[v] = v;
        } else {
            for (var v = 0; v < n; v++)
                V[v] = n - 1 - v;
        }

        var nv = n;
        var count = 2 * nv;
        for (int m = 0, v = nv - 1; nv > 2;) {
            if (count-- <= 0)
                return indices.ToArray();

            var u = v;
            if (nv <= u)
                u = 0;
            v = u + 1;
            if (nv <= v)
                v = 0;
            var w = v + 1;
            if (nv <= w)
                w = 0;

            if (Snip(u, v, w, nv, V)) {
                int a, b, c, s, t;
                a = V[u];
                b = V[v];
                c = V[w];
                indices.Add(a);
                indices.Add(b);
                indices.Add(c);
                m++;
                for (s = v, t = v + 1; t < nv; s++, t++)
                    V[s] = V[t];
                nv--;
                count = 2 * nv;
            }
        }

        indices.Reverse();
        return indices.ToArray();
    }

    private float Area()
    {
        var n = _mPoints.Count;
        var A = 0.0f;
        for (int p = n - 1, q = 0; q < n; p = q++) {
            var pval = _mPoints[p];
            var qval = _mPoints[q];
            A += pval.x * qval.y - qval.x * pval.y;
        }
        return A * 0.5f;
    }

    private bool Snip(int u, int v, int w, int n, int[] V)
    {
        int p;
        var A = _mPoints[V[u]];
        var B = _mPoints[V[v]];
        var C = _mPoints[V[w]];
        if (Mathf.Epsilon > (B.x - A.x) * (C.y - A.y) - (B.y - A.y) * (C.x - A.x))
            return false;
        for (p = 0; p < n; p++) {
            if (p == u || p == v || p == w)
                continue;
            var P = _mPoints[V[p]];
            if (InsideTriangle(A, B, C, P))
                return false;
        }
        return true;
    }

    private static bool InsideTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
    {
        float ax, ay, bx, by, cx, cy, apx, apy, bpx, bpy, cpx, cpy;
        float cCROSSap, bCROSScp, aCROSSbp;

        ax = C.x - B.x;
        ay = C.y - B.y;
        bx = A.x - C.x;
        by = A.y - C.y;
        cx = B.x - A.x;
        cy = B.y - A.y;
        apx = P.x - A.x;
        apy = P.y - A.y;
        bpx = P.x - B.x;
        bpy = P.y - B.y;
        cpx = P.x - C.x;
        cpy = P.y - C.y;

        aCROSSbp = ax * bpy - ay * bpx;
        cCROSSap = cx * apy - cy * apx;
        bCROSScp = bx * cpy - by * cpx;

        return aCROSSbp >= 0.0f && bCROSScp >= 0.0f && cCROSSap >= 0.0f;
    }
}

public class EarClipTriangle
{
	public Vector2 a;
	public Vector2 b;
	public Vector2 c;
	public Rect bounds;
	
	public EarClipTriangle(Vector2 a, Vector2 b, Vector2 c)
	{
		bounds = new Rect(a.x,a.y,0,0);
		Vector2[] points = new Vector2[]{a,b,c};
		for(int i=1; i<3; i++)
		{
			if(bounds.xMin < points[i].x)
				bounds.xMin = points[i].x;
			if(bounds.xMax < points[i].x)
				bounds.xMax = points[i].x;
			if(bounds.yMin < points[i].y)
				bounds.yMin = points[i].y;
			if(bounds.yMax < points[i].y)
				bounds.yMax = points[i].y;
		}
	}
}

public class EarClipper
{
	public static int[] Triangulate( Vector2[] points)
	{
		int numberOfPoints = points.Length;
		List<int> usePoints = new List<int>();
		for(int p=0; p<numberOfPoints; p++)
			usePoints.Add(p);
		int numberOfUsablePoints = usePoints.Count;
		List<int> indices = new List<int>();
		
        if (numberOfPoints < 3)
            return indices.ToArray();
		
		int it = 100;
		while(numberOfUsablePoints > 3)
		{
			for(int i=0; i<numberOfUsablePoints; i++)
			{
				int a,b,c;
				
				a=usePoints[i];
				
				if(i>=numberOfUsablePoints-1)
					b=usePoints[0];
				else
					b=usePoints[i+1];
				
				if(i>=numberOfUsablePoints-2)
					c=usePoints[(i+2)-numberOfUsablePoints];
				else
					c=usePoints[i+2];
				
				Vector2 pA = points[b];
				Vector2 pB = points[a];
				Vector2 pC = points[c];
				
				float dA = Vector2.Distance(pA,pB);
				float dB = Vector2.Distance(pB,pC);
				float dC = Vector2.Distance(pC,pA);
				
				float angle = Mathf.Acos((Mathf.Pow(dB,2)-Mathf.Pow(dA,2)-Mathf.Pow(dC,2))/(2*dA*dC))*Mathf.Rad2Deg * Mathf.Sign(Sign(points[a],points[b],points[c]));
				if(angle < 0)
				{
					continue;//angle is not reflex
				}
				
				bool freeOfIntersections = true;
				for(int p=0; p<numberOfUsablePoints; p++)
				{
					int pu = usePoints[p];
					if(pu==a || pu==b || pu==c)
						continue;
					
					if(IntersectsTriangle2(points[a],points[b],points[c],points[pu]))
					{
						freeOfIntersections=false;
						break;
					}
				}
				
				if(freeOfIntersections)
				{
					indices.Add(a);
					indices.Add(b);
					indices.Add(c);
					usePoints.Remove(b);
					it=100;
					numberOfUsablePoints = usePoints.Count;
					i--;
					break;
				}
			}
			it--;
			if(it<0)
				break;
		}
		
		indices.Add(usePoints[0]);
		indices.Add(usePoints[1]);
		indices.Add(usePoints[2]);
		indices.Reverse();
		
		return indices.ToArray();
	}
	
	private static bool IntersectsTriangle(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
	{
		bool b1, b2, b3;

		b1 = Sign(P, A, B) < 0.0f;
		b2 = Sign(P, B, C) < 0.0f;
		b3 = Sign(P, C, A) < 0.0f;
		
		return ((b1 == b2) && (b2 == b3));
	}
	
	private static float Sign(Vector2 p1, Vector2 p2, Vector2 p3)
	{
		return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
	}
					
	private static bool IntersectsTriangle2(Vector2 A, Vector2 B, Vector2 C, Vector2 P)
	{
			float planeAB = (A.x-P.x)*(B.y-P.y)-(B.x-P.x)*(A.y-P.y);
			float planeBC = (B.x-P.x)*(C.y-P.y)-(C.x - P.x)*(B.y-P.y);
			float planeCA = (C.x-P.x)*(A.y-P.y)-(A.x - P.x)*(C.y-P.y);
			return Sign(planeAB)==Sign(planeBC) && Sign(planeBC)==Sign(planeCA);
	}
	
	private static int Sign(float n) 
	{
		return (int)(Mathf.Abs(n)/n);
	}
}